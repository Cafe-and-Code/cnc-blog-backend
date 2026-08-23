const passport = require('passport');
const OpenIdConnectStrategy = require('passport-openidconnect').Strategy;
const jwt = require('jsonwebtoken');
const User = require('../app/models/User');

/**
 * Configure Authentik OIDC Strategy for Passport
 */
function configurePassport() {
  if (!process.env.AUTHENTIK_ISSUER || !process.env.AUTHENTIK_CLIENT_ID) {
    console.log('Authentik SSO is not configured (missing AUTHENTIK_ISSUER or AUTHENTIK_CLIENT_ID)');
    return;
  }

  passport.use(
    'authentik',
    new OpenIdConnectStrategy(
      {
        issuer: process.env.AUTHENTIK_ISSUER,

        authorizationURL: `${process.env.AUTHENTIK_ISSUER}authorize`,
        tokenURL: `${process.env.AUTHENTIK_ISSUER}token`,
        userInfoURL: `${process.env.AUTHENTIK_ISSUER}userinfo`,
        clientID: process.env.AUTHENTIK_CLIENT_ID,
        clientSecret: process.env.AUTHENTIK_CLIENT_SECRET,
        callbackURL: process.env.AUTHENTIK_CALLBACK_URL,
        scope: ['openid', 'profile', 'email'],
        passReqToCallback: true,
      },
      async (req, issuer, profile, cb) => {
        try {
          // Process user profile from Authentik
          const email = profile.emails?.[0]?.value || profile.email;
          const username = profile.displayName || profile.username || email.split('@')[0];
          const fullName = profile.displayName || profile.name?.formatted || username;
          const avatarImageUrl = profile.photos?.[0]?.value || '';

          // Find or create user by email
          let user = await User.findOne({ email });

          if (!user) {
            // Create new SSO user (no password required)
            const randomPassword = Math.random().toString(36).slice(-10) + Math.random().toString(36).toUpperCase().slice(-2) + '!';
            const bcrypt = require('bcrypt');
            const hashedPassword = await bcrypt.hash(randomPassword, 10);

            user = await User.create({
              username,
              email,
              password: hashedPassword,
              fullName,
              dateOfBirth: '2000-01-01', // Default value for SSO users
              avatarImageUrl: avatarImageUrl || 'https://api.dicebear.com/7.x/avataaars/svg?seed=' + username,
              authProvider: 'authentik',
              authProviderId: profile.id,
            });
          } else {
            // Update existing user with latest SSO info
            user.fullName = fullName;
            if (avatarImageUrl) {
              user.avatarImageUrl = avatarImageUrl;
            }
            if (!user.authProvider) {
              user.authProvider = 'authentik';
              user.authProviderId = profile.id;
            }
            await user.save();
          }

          // Generate JWT token
          const accessToken = jwt.sign(
            {
              user: {
                username: user.username,
                email: user.email,
                id: user.id,
              },
            },
            process.env.ACCESS_TOKEN_SECRET,
            { expiresIn: '30d' }
          );

          // Return user with token
          return cb(null, {
            user,
            token: accessToken,
          });
        } catch (error) {
          console.error('Authentik SSO Error:', error);
          return cb(error);
        }
      }
    )
  );

  // Serialize user to session
  passport.serializeUser((user, done) => {
    done(null, user);
  });

  // Deserialize user from session
  passport.deserializeUser((user, done) => {
    done(null, user);
  });
}

module.exports = { configurePassport };

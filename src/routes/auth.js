const express = require('express');
const passport = require('passport');
const router = express.Router();

const checkAuthentikConfigured = (req, res, next) => {
  if (!process.env.AUTHENTIK_ISSUER || !process.env.AUTHENTIK_CLIENT_ID) {
    return res.status(503).json({
      error: 'Authentik SSO is not configured on this server',
    });
  }
  next();
};

/**
 * @route   GET /auth/authentik
 * @desc    Initiate Authentik SSO login
 * @access  Public
 */
router.get(
  '/authentik',
  checkAuthentikConfigured,
  passport.authenticate('authentik', {
    scope: ['openid', 'profile', 'email'],
  })
);

/**
 * @route   GET /auth/authentik/callback
 * @desc    Handle Authentik SSO callback
 * @access  Public
 */
router.get(
  '/authentik/callback',
  checkAuthentikConfigured,
  passport.authenticate('authentik', {
    failureRedirect: `${process.env.APP_URL}/login?error=auth_failed`,
    session: false,
  }),
  (req, res) => {
    // Successful authentication, redirect with token
    const { token, user } = req.user;
    const redirectUrl = `${process.env.APP_URL}/auth/sso-callback?token=${token}&userId=${user.id}`;
    res.redirect(redirectUrl);
  }
);

/**
 * @route   GET /auth/sso-callback
 * @desc    Validate SSO token and create session (frontend calls this)
 * @access  Public
 */
router.get('/sso-callback', (req, res) => {
  const { token, userId } = req.query;
  if (!token || !userId) {
    return res.status(400).json({ error: 'Missing token or userId' });
  }
  res.json({ token, userId, success: true });
});

/**
 * @route   GET /auth/logout
 * @desc    Logout user
 * @access  Public
 */
router.get('/logout', (req, res) => {
  req.logout((err) => {
    if (err) {
      console.error('Logout error:', err);
      return res.status(500).json({ error: 'Logout failed' });
    }
    res.json({ message: 'Logged out successfully' });
  });
});

module.exports = router;

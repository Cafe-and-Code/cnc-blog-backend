const express = require("express");
const path = require("path");
require("dotenv").config();
const port = process.env.PORT || 3001;
const hostName = process.env.HOST_NAME || "0.0.0.0";
const morgan = require("morgan");
const methodOverride = require("method-override");
const session = require("express-session");
const passport = require("passport");
const cookieParser = require("cookie-parser");
const cors = require("cors");
const app = express();
const errorHandle = require("./app/middleware/errorHandle");

// Import passport configuration
const { configurePassport } = require("./config/passport");

const route = require("./routes");
const db = require("./config/db");

// Configure CORS
app.use(
  cors({
    origin: process.env.CORS_ORIGIN || "http://localhost:3000",
    credentials: true,
  })
);

// Cookie parser
app.use(cookieParser());

// Session configuration for OAuth
app.use(
  session({
    secret: process.env.SESSION_SECRET || "default-session-secret",
    resave: false,
    saveUninitialized: false,
    cookie: {
      secure: process.env.NODE_ENV === "production",
      httpOnly: true,
      maxAge: 24 * 60 * 60 * 1000, // 24 hours
    },
  })
);

// Initialize Passport
app.use(passport.initialize());
app.use(passport.session());
configurePassport();

// Connect to database
db.connect();

const fs = require("fs");
const imagesDir = path.join(__dirname, "images");
if (!fs.existsSync(imagesDir)) {
  fs.mkdirSync(imagesDir, { recursive: true });
}

// Cho phép public thư mục images ra ngoài
app.use("/images", express.static(imagesDir));

app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// Override with POST having ?_method=DELETE or ?_method=PUT
app.use(methodOverride("_method"));

// HTTP logger
app.use(morgan("combined"));

// Health check endpoint
app.get("/health", (req, res) => {
  res.status(200).json({ status: "ok", uptime: process.uptime() });
});

// Auth routes (must be before general routes)
const authRoutes = require("./routes/auth");
app.use("/auth", authRoutes);

// Routes init
route(app);

// Custom error handling middleware (must be after all routes)
app.use(errorHandle);

app.listen(port, hostName, () => {
  console.log(`App listening on port ${port}`);
  console.log(`Authentik SSO enabled: ${!!process.env.AUTHENTIK_ISSUER}`);
});

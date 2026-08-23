const mongoose = require("mongoose");
const Schema = mongoose.Schema;
const autoIncrement = require("mongoose-sequence")(mongoose);
const User = new Schema(
  {
    id: { type: Number },
    username: {
      type: String,
      maxLength: 255,
      required: [true, "please add the user name"],
    },
    email: {
      type: String,
      maxLength: 255,
      required: [true, "please add the user email address"],
      unique: [true, "Email address already taken"],
    },
    password: {
      type: String,
      maxLength: 255,
      // Required only for non-SSO users
    },
    fullName: {
      type: String,
      maxLength: 255,
      required: [true, "please add the user fullName"],
    },
    dateOfBirth: {
      type: String,
      maxLength: 255,
      // Optional for SSO users
    },
    avatarImageUrl: {
      type: String,
      maxLength: 255,
    },
    // SSO Authentication Fields
    authProvider: {
      type: String,
      enum: ['local', 'authentik', 'google', 'github', null],
      default: 'local',
    },
    authProviderId: {
      type: String,
      maxLength: 255,
    },
    // SSO users may not have a local password
    isSSOUser: {
      type: Boolean,
      default: false,
    },
  },
  {
    timestamps: true,
  }
);

// Compound index for SSO lookup
User.index({ authProvider: 1, authProviderId: 1 }, { sparse: true });

User.plugin(autoIncrement, { inc_field: "id" });

module.exports = mongoose.model("User", User);

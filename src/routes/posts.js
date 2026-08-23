const express = require("express");
const router = express.Router();
const postsController = require("../app/controllers/PostsController");
const validateToken = require("../app/middleware/validateTokenHandler");

router.get("/", postsController.Posts);
router.get("/:slug", postsController.PostDetail);
router.post("/", validateToken, postsController.Create);

module.exports = router;

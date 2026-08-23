const postsRouter = require("./posts");
const meRouter = require("./me");
const aboutRouter = require("./about");
const blogRouter = require("./blogs");
const userRouter = require("./users");
const uploadRouter = require("./upload");
const categoryRouter = require("./category");

function route(app) {
  app.use("/api/Posts", postsRouter);
  app.use("/api/Me", meRouter);
  app.use("/api/About", aboutRouter);
  app.use("/api/Blogs", blogRouter);
  app.use("/api/Users", userRouter);
  app.use("/api/Upload", uploadRouter);
  app.use("/api/Categories", categoryRouter);
}

module.exports = route;

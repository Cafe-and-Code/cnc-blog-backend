const Blog = require("../models/Blog");
const { multiplemongooseToObject } = require("../../util/mongoose");

class MeController {
  // [GET] /stored/blogs
  storedBlogs(req, res, next) {
    Promise.all([
      Blog.find({}),
      Blog.countDocumentsWithDeleted({
        deleted: true,
      }),
    ])
      .then(([blogs, deletedCount]) => {
        res.json({
          deletedCount,
          blogs: multiplemongooseToObject(blogs),
        });
      })
      .catch(next);
  }
  // [GET] /trash/blogs
  trashBlogs(req, res, next) {
    Blog.findWithDeleted({ deleted: true })
      .then((blogs) => {
        res.json({
          blogs: multiplemongooseToObject(blogs),
        });
      })
      .catch(next);
  }
}

module.exports = new MeController();


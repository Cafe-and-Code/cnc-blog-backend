const Post = require("../models/Post");
const { mongooseToObject } = require("../../util/mongoose");
const { multiplemongooseToObject } = require("../../util/mongoose");
const { formatSlug } = require("../../util/format");
class NewsController {
  // [GET] /Posts
  async Posts(req, res, next) {
    try {
      const { pageNumber, pageSize } = req.query;
      const posts = await Post.find().sort({ createdAt: -1 });
      const currentPage = Number(pageNumber);
      const perPage = Number(pageSize);
      const totalItems = posts.length;
      const totalPage = Math.ceil(totalItems / perPage);
      const startIndex = (currentPage - 1) * perPage;
      const endIndex = startIndex + perPage;

      const paggingData = posts.slice(startIndex, endIndex);
      if (pageNumber && pageSize) {
        res.json({
          posts: paggingData,
          totalPosts: posts.length,
          totalPage,
          currentPage,
        });
      } else {
        res.json({
          posts: posts,
        });
      }
    } catch (err) {
      next(err);
    }
  }

  // [GET] /Post Detail
  async PostDetail(req, res, next) {
    try {
      const postItem = await Post.findOne({
        slug: formatSlug(req.params.slug),
      });
      if (!postItem) {
        res.status(404).json({ status: 404, message: "Post not found!" })
      }
      res.json(mongooseToObject(postItem));
    } catch (err) {
      next(err);
    }
  }

  async Create(req, res, next) {
    const { title, content, description, image, categories } = req.body;
    if (!title || !content || !description || !image || !categories) {
      res
        .status(404)
        .json({ status: 404, message: "All fields are required!" });
    }
    try {
      const newPost = await Post.create({
        title,
        content,
        description,
        image,
        categories,
        user_id: req.user.id, // Lấy user_id từ req.user
      });

      res.status(201).json(newPost);
      res.status(201).json({
        message: "Post created successfully",
        post: newPost
      });
    } catch (error) {
      next(error);
    }
  }
}

module.exports = new NewsController();

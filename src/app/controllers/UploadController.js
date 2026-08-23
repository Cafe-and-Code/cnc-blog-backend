class UploadController {
  async image(req, res, next) {
    try {
      if (!req.file) {
        return res.status(400).json({ status: 400, message: "No file uploaded" });
      }
      let baseUrl =
        process.env.BACKEND_URL ||
        `${req.protocol}://${req.get("host")}`;
      baseUrl = baseUrl.trim().replace(/\/+$/, "");
      const fileUrl = `${baseUrl}/images/${req.file.filename}`;
      res.json({
        message: "Upload success",
        filePath: fileUrl,
      });
    } catch (err) {
      next(err);
    }
  }
}

module.exports = new UploadController();

class AboutController {
  // [GET] /about
  index(req, res) {
    res.json({
      status: "ok",
      name: "CNC Blog Backend API",
      timestamp: new Date().toISOString(),
    });
  }
}

module.exports = new AboutController();


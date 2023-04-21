const express = require("express");

const router = express.Router();

router.get("/healthcheck", async (req, res) => {
  res.send({
    status: "Healthy",
  });
});

module.exports = router;

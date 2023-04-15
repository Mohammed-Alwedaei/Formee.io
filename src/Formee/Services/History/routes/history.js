const express = require("express");
const History = require("../models/History");

const router = express.Router();

router.get("/all/:userId", async (req, res) => {
  try {
    const userId = req.params.userId;
    const pageNumber = req.query.pageNumber;
    const nPerPage = req.query.nPerPage;

    const query = await History.find({ userId })
      .sort({ createdDate: 1 })
      .skip(pageNumber > 0 ? (pageNumber - 1) * nPerPage : 0)
      .limit(nPerPage);

    res.send(query);
  } catch (e) {
    console.error(e);
  }
});

router.get("/all/:service", async (req, res) => {
  try {
    var service = req.params.service;

    res.send(
      await History.find({ service: { $regex: service, $options: "i" } })
    );
  } catch (e) {
    console.error(e);
  }
});

router.post("/", async (req, res) => {
  const historyModel = new History({
    title: req.body.title,
    action: req.body.action,
    actionBy: req.body.actionBy,
  });

  try {
    let savedHistory = await historyModel.save();
    res.send(savedHistory);
  } catch (e) {
    res.send("rejected");
  }
});

module.exports = router;

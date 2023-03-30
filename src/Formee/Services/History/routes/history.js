const express = require("express");
const History = require("../models/History");

const router = express.Router();

router.get("/all", async (req, res) => {
  try {
    res.send(await History.find());
  } catch (e) {
    console.error(e);
  }
});

router.get("/all/:service", async (req, res) => {
  try {
    var service = req.params.service;

    console.log(service.charAt(0).toUpperCase());

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

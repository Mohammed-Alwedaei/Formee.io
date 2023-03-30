const mongoose = require("mongoose");
const Schema = mongoose.Schema;
const ObjectId = Schema.ObjectId;

const History = new Schema({
  id: ObjectId,
  title: String,
  action: String,
  userId: String,
  service: String,
  requestDate: Date,
  createdDate: { type: Date, default: Date.now },
});

module.exports = mongoose.model("History", History);

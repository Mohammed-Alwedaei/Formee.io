const express = require("express");
const mongoose = require("mongoose");
const bodyParser = require("body-parser");

require("dotenv").config();

const ServiceBusConsumer = require("./utilities/ServiceBusConsumer");

const app = express();

const historyRoutes = require("./routes/history");

app.use(bodyParser.json());

app.get("/", (req, res) => {
  console.log("Working in history service");

  res.send("Hello, History");
});

app.use("/api/history", historyRoutes);

mongoose.connect(process.env.MONGO_DB_HISTORY);

ServiceBusConsumer.Main();

app.listen(3000);

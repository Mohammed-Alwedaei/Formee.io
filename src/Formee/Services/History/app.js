const express = require("express");
const mongoose = require("mongoose");
const bodyParser = require("body-parser");
const historyRoutes = require("./routes/history");
const healthcheckRoutes = require("./routes/healthcheck");
const ServiceBusConsumer = require("./utilities/ServiceBusConsumer");

require("dotenv").config();

const app = express();

app.use(bodyParser.json());

app.get("/", (req, res) => {
  console.log("Working in history service");

  res.send("Hello, History");
});

app.use("/api/history", historyRoutes);
app.use("/", healthcheckRoutes);

mongoose.connect(process.env.MONGO_DB_HISTORY);

ServiceBusConsumer.Main();

app.listen(3000);

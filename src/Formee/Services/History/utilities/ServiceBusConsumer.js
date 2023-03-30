const { ServiceBusClient } = require("@azure/service-bus");

const History = require("../models/History");

const connectionString = process.env.AZURE_SERVICE_BUS;

const topicName = "topic-history";
const subscriptionName = "formeeaAnalytics";

async function Main() {
  const serviceBusClient = new ServiceBusClient(connectionString);

  const serviceBusReceiver = serviceBusClient.createReceiver(
    topicName,
    subscriptionName
  );

  const messageHandler = async (message) => {
    const messageBody = message.body;

    var historyModel = new History({
      title: messageBody.History.Title,
      action: messageBody.History.Action,
      userId: messageBody.History.UserId,
      service: messageBody.History.Service,
      requestDate: messageBody.CreatedDate,
    });

    await historyModel.save();
  };

  const errorHandler = async (error) => {
    console.log(error);
  };

  serviceBusReceiver.subscribe({
    processMessage: messageHandler,
    processError: errorHandler,
  });
}

module.exports = { Main };

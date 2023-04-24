const { ServiceBusClient } = require("@azure/service-bus");

const History = require("../models/History");

async function Main() {
  const connectionString = process.env.AZURE_SERVICE_BUS;

  const topicName = "topic-history";
  const subscriptionName = "subscription-history";
  const serviceBusClient = new ServiceBusClient(connectionString);

  const serviceBusReceiver = serviceBusClient.createReceiver(
    topicName,
    subscriptionName
  );

  const messageHandler = async (message) => {
    const messageBody = message.body;

    var historyModel = new History({
      title: messageBody.Entity.Title,
      action: messageBody.Entity.Action,
      userId: messageBody.Entity.UserId,
      service: messageBody.Entity.Service,
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

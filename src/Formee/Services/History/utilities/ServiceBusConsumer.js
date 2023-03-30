const { ServiceBusClient } = require("@azure/service-bus");
const { DefaultAzureCredential } = require("@azure/identity");

const connectionString =
  "Endpoint=sb://formee.servicebus.windows.net/;SharedAccessKeyName=SharedAccessPolicy;SharedAccessKey=2zjgyBu0KGgdOiVx8rHy5GPnHBNvKoR9d+ASbDEu2fw=;EntityPath=topic-history";

const queueName = "topic-history";

const azureAccountCredentials = new DefaultAzureCredential();

async function Main() {
  const serviceBusClient = new ServiceBusClient(connectionString);

  const serviceBusReceiver = serviceBusClient.createReceiver(queueName);

  const messageHandler = async (message) => {
    console.log(`receive message: ${message.body}`);
  };

  const errorHandler = async (error) => {
    console.log(error);
  };

  receiver.subscribe({
    processMessage: messageHandler,
    processError: errorHandler,
  });
}

main().catch((err) => {
  console.log("Error occurred: ", err);
  process.exit(1);
});

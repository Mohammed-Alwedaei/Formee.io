FROM node:alpine

WORKDIR /app

EXPOSE 3000

COPY ./Services/History/package*.json ./

RUN npm install

COPY ./Services/History ./

CMD ["npm", "start"]
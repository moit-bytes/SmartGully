const mongoose = require('mongoose');
const express = require('express');
const helmet = require('helmet');

const request = require('request');
const bodyParser = require("body-parser");
const cors = require("cors");

const app = express();
const port = process.env.PORT || 8002;
const webhookURL = "https://discordapp.com/api/webhooks/665242505880535051/Eie2DVC117NuWFahoVyQjipyIqPoacMR__EEPEF_-YJWGf9C30JnluIq5NKdHafn7WS9";

app.use(helmet());
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({
  extended: true
}));
app.use(cors());

mongoose
  .connect(process.env.MONGO_URL, {
    useNewUrlParser: true,
    useUnifiedTopology: true
  })
  .then(() => console.log("Connected to the database"));

const authRoute = require("./routes/authRoute");
const powerRoute = require("./routes/powerRoute");
const ledStatusRoute = require("./routes/ledStatusRoute");

const value = [];

app.use("/auth", authRoute);
app.use("/power", powerRoute);
app.use("/ledstatus", ledStatusRoute);

app.get('/', (req, res) => {
	res.send('yolo');
});

app.post('/discord', (req, res) => {
	console.log(req.body);
	value.push(req.body.led);
	request.post({
		headers: {'content-type': 'application/json'},
		url: webhookURL,
		body: {
				"username": "UpTimer",
				"content": "Power consumption",
				"avatar_url": "https://www.w3schools.com/w3images/avatar2.png",
				"embeds": [
					{
						"title": `Power Consumption`,
						"color": '3201040',
						"description": `Power Comsumption Report: ${req.body.led}`
					}
				]
			},
		json: true
	});
	res.send("success");
});

app.get('/ledstatus', (req, res) => {
	res.json({isActive: true});
});
app.post('/ledstatus', (req, res) => {
	console.log(req.body)
	res.send('success');
})

app.listen(port, () => {
	console.log(`listening on ${port}`);
});

// 0.0167
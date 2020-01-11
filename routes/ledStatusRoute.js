const route = require('express').Router();
const jwt = require('jsonwebtoken');

const LedStatusService = require('../Services/Power.service');

const { verifyToken } = require('../utils/verifyToken');


route.get('/', async (req, res) => {
	// jwt.verify(req.token, process.env.JWT_SECRET, async (err, response) => {
	// 	if(err) throw err;
	// 	if(response) {
			const powerData = await LedStatusService.getLed();
			const latestPowerData = powerData[powerData.length - 1];
			console.log('tapped ------>>' + latestPowerData);
			res.status(200).json({power: latestPowerData.power, addedOn: latestPowerData.addedOn, isLoggedIn: true});
		// } else {
			// res.status(403).json({isLoggedIn: false});
		// }
	// });
});

route.post('/', async (req, res) => {
	const newPowerData = await LedStatusService.createLed(req.body.isActive, req.body.location);
	console.log("Led --------> ", req.body);
	res.send('yolo');
})

module.exports = route;
const route = require('express').Router();
const jwt = require('jsonwebtoken');

const LedStatusService = require('../Services/LedStatus.service');

const { verifyToken } = require('../utils/verifyToken');


route.post('/ledactive', async (req, res) => {
	// jwt.verify(req.token, process.env.JWT_SECRET, async (err, response) => {
	// 	if(err) throw err;
	// 	if(response) {
			const ledData = await LedStatusService.getLed(req.body.location);
			console.log('tapped ------>>' + ledData[0]);
			res.status(200).json({isActive: ledData[0].isActive, isLoggedIn: true});
		// } else {
			// res.status(403).json({isLoggedIn: false});
		// }
	// });
});

route.post('/', async (req, res) => {
	const powerData = {
		isActive: req.body.isActive,
		location: req.body.location
	}
	const newPowerData = await LedStatusService.createLed(req.body.isActive, req.body.location);
	console.log("Led --------> ", req.body);
	res.send('yolo');
})

module.exports = route;
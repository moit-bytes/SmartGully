const route = require('express').Router();
const jwt = require('jsonwebtoken');

const powerConsumption = require('../Services/powerConsumption.service');

const { verifyToken } = require('../utils/verifyToken');


route.get('/', verifyToken, (req, res) => {
	jwt.verify(req.token, process.env.JWT_SECRET, async (err, response) => {
		if(err) throw err;
		if(response) {
			const lostFoundData = await power.getLostFound();
			res.status(200).json({lostFoundData, isLoggedIn: true, message: "Operation Successful"});
		} else {
			res.status(403).json({isLoggedIn: false, message: "Invalid token"});
		}
	});
});

route.post('/', verifyToken, (req, res) => {
	const reqBody = req.body;
	console.log(req.body)
	jwt.verify(req.token, process.env.JWT_SECRET, async (err, response) => {
		if(err) throw err;
		if(response) {
			const lostFoundDetails = {
				authorId: reqBody.authorId, //authors regNumber
				content: {
					contentHeading: reqBody.content.head,
					contentBody: reqBody.content.body,
					contact: reqBody.content.contact
				},
				priority: 0,
				status: reqBody.status
			}
			const lostFound = await LostFoundService.createLostFound({...lostFoundDetails});
			res.status(200).json({isLoggedIn: true, newEntry: lostFound, message: 'Operation Successful'});
		} else {
			res.status(403).json({isLoggedIn: false, newEntry: {}, message: 'Invalid token'});
		}
	});
});

module.exports = route;
const route = require("express").Router();
const jwt = require("jsonwebtoken");
// const fetch = require("node-fetch");
const bcrypt = require("bcrypt");
const UserService = require("../Services/User.service");

const { verifyToken } = require("../utils/verifyToken");

route.post("/", async (req, res) => {
	let username = req.body.username;
	let password = req.body.password;
	console.log(username)
	console.log(password)

	const user = await UserService.findUser(username);
	// bcrypt.hash('password', 10, (err, hash) => {
	// 	console.log(hash);
	// });
	if(user) {

		bcrypt.compare(password, user.passwordHash, (err, compareResult) => {
			if(err) throw err;
			if(compareResult) {
				console.log('password matched');
				const userProfile = {
					username: user.username,
					location: user.location
				}
				console.log(user)
				jwt.sign({username: user.username}, process.env.JWT_SECRET, {expiresIn: '24h'}, (err, token) => {
					if(err) throw err;
					//send jwt token, profile along with the response
					res.status(200).json({
						token, 
						username: user.username, 
						location: user.location,
						isLoggedIn: true
					});
				});
			} else {
				//wrong password
				//future implementation -> send notification to user if it's admin
				res.status(400).json({isLoggedIn: false, message: "Wrong Credentials"});
			}
		});
	} else {
		res.status(400).json({isLoggedIn: false, message: "Wrong Credentials"});
	}
});

route.post('/verifyoldtoken', verifyToken, (req, res) => {
	jwt.verify(req.token, process.env.JWT_SECRET, (err, response) => {
		if(err) console.log(err);
		if(response) {
			res.status(200).json({isLoggedIn: true, message: "Token still valid"});
		} else {
			res.status(403).json({isLoggedIn: false, message: "Token Invalid"});
		}
	})
});

route.get('/verify', verifyToken, (req, res) => {
	console.log(req.token)
	jwt.verify(req.token, process.env.JWT_SECRET, (err, response) => {
		if(err) console.log(err);
		if(response) {
			res.json({isLoggedIn: true});
		} else {
			res.json({isLoggedIn: false});
		}
	})
});

module.exports = route;
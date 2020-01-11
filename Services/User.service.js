const User = require('../models/User.model');

class UserService {
	constructor(logger) {
		this.logger = console;
	}

	// returns user if there exist one in the db
	async findUser(username) {
		try {
			const user = await User.findOne({ username }).exec();
			return user;
		} catch(error) {
			this.logger.error(`findUser: ${ error }`);
		}
	}

}

module.exports = new UserService;
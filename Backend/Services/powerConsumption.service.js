const PowerConsumption = require('../models/PowerConsumption.model');

class UserService {
	constructor(logger) {
		this.logger = console;
	}

	async createPowerConsumption(username) {
		try {
			const newPower = new PowerConsumption({...lostFoundDetails});
			newPower.save();
			return newPower;
		} catch(error) {
			this.logger.error(`findUser: ${ error }`);
		}
	}

}

module.exports = new UserService;
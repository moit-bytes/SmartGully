const Led = require('../models/Led.model');

class LedStatusService {
	constructor(logger) {
		this.logger = console;
	}

	async getLed() {
		try {
			const power = await Led.find({}).sort({'creationDate': -1}).exec();
			return power;
		} catch(error) {
			this.logger.error(`getLed: ${ error }`);
		}
	}

	async createLed(isActive, location) {
		console.log(location)
		const editedLostFound = await Led.findOneAndUpdate(
			{ location },
			{ isActive },
			{ new: true }
		).exec();
		return editedLostFound;
	}

}

module.exports = new LedStatusService;
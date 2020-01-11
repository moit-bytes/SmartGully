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
		try {
			const ledData = await new Led({location}, {isActive}, {
				upsert: true
			});
			ledData.save();
			return ledData;
		} catch(error) {
			this.logger.error(`createLed: ${ error }`);
		}
	}

}

module.exports = new LedStatusService;
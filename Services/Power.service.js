const Power = require('../models/Power.model');

class PowerService {
	constructor(logger) {
		this.logger = console;
	}

	async getPower() {
		try {
			const power = await Power.find({}).sort({'creationDate': -1}).exec();
			return power;
		} catch(error) {
			this.logger.error(`getPower: ${ error }`);
		}
	}

	async createPower(power) {
		try {
			const powerData = new Power({power});
			powerData.save();
			return powerData;
		} catch(error) {
			this.logger.error(`createPower: ${ error }`);
		}
	}

}

module.exports = new PowerService;
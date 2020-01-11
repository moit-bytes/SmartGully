const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const PowerConsumptionSchema = new Schema({
	power: Number,
	addedOn: Date
});

module.exports = mongoose.model('User', PowerConsumptionSchema, 'powerConsumption'); //Params -> Model, Schema, CollectionName
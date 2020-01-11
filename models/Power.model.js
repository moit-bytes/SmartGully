const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const PowerSchema = new Schema({
	power: String,
	location: String
});

module.exports = mongoose.model('Power', PowerSchema, 'power'); //Params -> Model, Schema, CollectionName
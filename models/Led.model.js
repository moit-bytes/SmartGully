const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const LedSchema = new Schema({
	isActive: Boolean,
	location: String
});

module.exports = mongoose.model('Led', LedSchema, 'led'); //Params -> Model, Schema, CollectionName
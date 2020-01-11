const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const UserSchema = new Schema({
	username: String,
	location: String,
	passwordHash: String
});

module.exports = mongoose.model('User', UserSchema, 'user'); //Params -> Model, Schema, CollectionName
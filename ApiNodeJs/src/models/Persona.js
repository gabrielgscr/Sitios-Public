const { DataTypes } = require('sequelize');
const sequelize = require('../config/database');

const Persona = sequelize.define('Persona', {
    PersonaID: {
        type: DataTypes.STRING(50),
        primaryKey: true
    },
    Nombre: {
        type: DataTypes.STRING(50),
        allowNull: false
    },
    Tipo: {
        type: DataTypes.TINYINT,
        allowNull: false
    },
    Gender: {
        type: DataTypes.STRING(10),
        defaultValue: ''
    },
    Password: {
        type: DataTypes.STRING(100),
        defaultValue: ''
    }
}, {
    tableName: 'Persona',
    timestamps: false
});

module.exports = Persona;

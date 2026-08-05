const { Sequelize } = require('sequelize');
require('dotenv').config();

const sequelize = new Sequelize(process.env.DB_NAME, process.env.DB_USER, process.env.DB_PASS, {
    host: process.env.DB_HOST,
    dialect: 'mssql',
    dialectOptions: {
        options: {
            encrypt: false, // Desactivar SSL si es local
            trustServerCertificate: true,
        },
    },
    logging: false, // Desactiva logs de SQL
});

sequelize.authenticate()
    .then(() => console.log('✅ Conectado a SQL Server'))
    .catch(err => console.error('❌ Error en la conexión:', err));

module.exports = sequelize;

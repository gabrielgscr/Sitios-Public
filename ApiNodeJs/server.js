// const express = require('express');
// const app = express();
// const port = 3000;

// app.use(express.json()); // Permite recibir JSON en las peticiones

// app.get('/', (req, res) => {
//     res.send('¡Hola, mundo desde Node.js!');
// });

// app.listen(port, () => {
//     console.log(`Servidor corriendo en http://localhost:${port}`);
// });


require('dotenv').config();
const app = require('./src/app');
const sequelize = require('./src/config/database');

const PORT = process.env.PORT || 3000;

sequelize.sync()
    .then(() => console.log("✅ Base de datos sincronizada"))
    .catch(err => console.error("❌ Error al sincronizar:", err));

app.listen(PORT, () => {
    console.log(`🚀 Servidor corriendo en http://localhost:${PORT}`);
});

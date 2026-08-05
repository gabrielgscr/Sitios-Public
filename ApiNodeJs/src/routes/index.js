const express = require('express');
const personaRoutes = require('./personaRoutes');

const router = express.Router();
router.use('/personas', personaRoutes);

module.exports = router;

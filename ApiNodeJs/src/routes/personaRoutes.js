const express = require('express');
const { getAllPersonas, getPersonaById, createPersona, updatePersona, deletePersona } = require('../controllers/personaController');

const router = express.Router();

router.get('/', getAllPersonas);
router.get('/:id', getPersonaById);
router.post('/', createPersona);
router.put('/:id', updatePersona);
router.patch('/:id', updatePersona);
router.delete('/:id', deletePersona);

module.exports = router;

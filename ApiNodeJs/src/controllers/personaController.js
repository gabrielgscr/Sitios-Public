const Persona = require('../models/Persona');

const sanitizePersona = (personaInstance) => {
    const persona = personaInstance.get({ plain: true });
    delete persona.Password;
    return persona;
};

const sendSequelizeError = (res, error) => {
    if (error.name === 'SequelizeUniqueConstraintError') {
        return res.status(409).json({ error: 'Conflicto: recurso duplicado' });
    }

    if (error.name === 'SequelizeValidationError') {
        return res.status(422).json({ error: 'Datos invalidos para la entidad' });
    }

    return res.status(500).json({ error: 'Error interno del servidor' });
};


const getAllPersonas = async (req, res) => {
    try {
        const personas = await Persona.findAll();
        res.json(personas.map(sanitizePersona));
    } catch (error) {
        sendSequelizeError(res, error);
    }
};


const getPersonaById = async (req, res) => {
    try {
        const persona = await Persona.findByPk(req.params.id);
        if (!persona) return res.status(404).json({ message: "Persona no encontrada" });
        res.json(sanitizePersona(persona));
    } catch (error) {
        sendSequelizeError(res, error);
    }
};


const createPersona = async (req, res) => {
    try {
        const nuevaPersona = await Persona.create(req.body);
        res.location(`/api/personas/${nuevaPersona.PersonaID}`);
        res.status(201).json(sanitizePersona(nuevaPersona));
    } catch (error) {
        sendSequelizeError(res, error);
    }
};


const updatePersona = async (req, res) => {
    try {
        const isPut = req.method === 'PUT';
        const { id } = req.params;
        const { PersonaID, Nombre, Tipo, Gender, Password } = req.body;

        if (PersonaID && PersonaID !== id) {
            return res.status(400).json({ error: 'PersonaID debe coincidir con el id de la URL' });
        }

        const persona = await Persona.findByPk(req.params.id);
        if (!persona) return res.status(404).json({ message: "Persona no encontrada" });

        if (isPut) {
            if (Nombre === undefined || Tipo === undefined) {
                return res.status(400).json({ error: 'PUT requiere un recurso completo (Nombre y Tipo son obligatorios)' });
            }

            await persona.update({
                Nombre,
                Tipo,
                Gender: Gender ?? '',
                Password: Password ?? ''
            });
        } else {
            await persona.update(req.body);
        }

        res.json(sanitizePersona(persona));
    } catch (error) {
        sendSequelizeError(res, error);
    }
};


const deletePersona = async (req, res) => {
    try {
        const persona = await Persona.findByPk(req.params.id);
        if (!persona) return res.status(404).json({ message: "Persona no encontrada" });

        await persona.destroy();
        res.status(204).send();
    } catch (error) {
        sendSequelizeError(res, error);
    }
};

module.exports = { getAllPersonas, getPersonaById, createPersona, updatePersona, deletePersona };

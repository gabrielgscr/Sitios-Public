const deleteModal = document.getElementById('deleteModal');

if (deleteModal) {
    deleteModal.addEventListener('show.bs.modal', event => {
        const button = event.relatedTarget;

        document.getElementById('deleteForm').action = button.dataset.deleteUrl;
        document.getElementById('deletePersonName').textContent = button.dataset.personName;
    });
}


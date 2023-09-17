// site.js

// Función para eliminar un casco
function eliminarCasco(idCasco) {
    // Mostrar un cuadro de confirmación para asegurarse de que el usuario realmente quiere eliminar el casco
    if (confirm("¿Estás seguro que deseas eliminar este casco?")) {
        // Hacer una solicitud AJAX para eliminar el casco
        $.ajax({
            url: '/Cascos/Delete/' + idCasco, // Asegúrate de que esta ruta sea correcta según tu enrutamiento
            type: 'POST',
            success: function (data) {
                // Manejar la respuesta del servidor
                if (data.success) {
                    // Si la eliminación fue exitosa, redirigir a la página de lista de cascos
                    window.location.href = '/Cascos/Index'; // Cambia la ruta según tu enrutamiento
                } else {
                    // Si hubo un error, mostrar un mensaje de error
                    alert('Error al eliminar el casco.');
                }
            },
            error: function () {
                // Manejar errores de la solicitud AJAX
                alert('Error de conexión al eliminar el casco.');
            }
        });
    }
}
function editarCascoBtn(idCasco) {
    console.log('Editar casco llamado con ID: ' + idCasco);
    window.location.href = '@Url.Action("Edit", "Cascos")/' + idCasco;
}

function editarCasco(idCasco) {
    // Redirige a la página de edición con el ID del casco
    window.location.href = `/Cascos/Edit/${idCasco}`;
}

    
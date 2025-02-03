SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oconexion);
cmd.Parameters.AddWithValue("IdCategoria", id);
cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 250).Direction = ParameterDirection.Output;


// boton eliminar
<a href="#" class="btn btn-danger btn-sm" data-bs-toggle="modal" data-bs-target="#confirmModal"
   data-url="@Url.Action("Eliminar", "Producto", new { id = item.IdProducto })">
    <i class="fas fa-trash"></i> Eliminar
</a>


<a href="@Url.Action("Eliminar", "Producto", new { id = item.IdProducto })" class="btn btn-danger btn-sm">
    <i class="fas fa-trash"></i> Eliminar
</a>


<div class="modal fade" id="confirmModal" tabindex="-1" aria-labelledby="modalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalLabel">Confirmar eliminación</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                ¿Está seguro de que desea eliminar esta categoría?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                <a href="#" id="confirmDeleteButton" class="btn btn-danger">Eliminar</a>
            </div>
        </div>
    </div>
</div>


<script>
    document.addEventListener('DOMContentLoaded', function () {
        var confirmModal = document.getElementById('confirmModal');
        confirmModal.addEventListener('show.bs.modal', function (event) {
            var button = event.relatedTarget;
            var deleteUrl = button.getAttribute('data-url');
            var confirmButton = document.getElementById('confirmDeleteButton');
            confirmButton.setAttribute('href', deleteUrl);
        });
    });
</script>



TempData["Success"] = "Registrado exitosamente.";
TempData["Error"] = mensaje;


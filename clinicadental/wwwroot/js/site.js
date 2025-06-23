document.addEventListener("DOMContentLoaded", function () {
    let botones = document.querySelectorAll('.sidebarizq');

    let botonActivo = Array.from(botones).find(boton =>
        boton.dataset.controller === currentController
    );

    // Limpiar clases previas
    botones.forEach(el => el.classList.remove('active'));

    if (botonActivo) {
        botonActivo.classList.add('active');
    }

    // Opcional: si quieres que al hacer clic se guarde la selección por sesión
    botones.forEach(boton => {
        boton.addEventListener('click', function () {
            botones.forEach(el => el.classList.remove('active'));
            this.classList.add('active');
        });
    });

    const botonesAceptar = document.querySelectorAll(".btn-bloquear");

    botonesAceptar.forEach(btn => {
        btn.addEventListener("click", function () {
            btn.disabled = true;
            const textoOriginal = btn.innerText;
            btn.innerText = "Procesando...";

            // Solo si no hay redirección, reactivar el botón
            setTimeout(function () {
                btn.disabled = false;
                btn.innerText = textoOriginal;
            }, 4000); // 5 segundos
        });
    });
 
});

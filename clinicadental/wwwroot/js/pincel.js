document.addEventListener('DOMContentLoaded', () => {    

    const camadaPincel = document.querySelector('#camadaPincel')
    const contexto = camadaPincel.getContext('2d')
   


    document.querySelector("#mouse").addEventListener('change', function() {
        usaPincel()
    })

    camadaPincel.onmousedown = () => {
        if (usaPincel()) {
            pincel.ativo = true
        } else if (usaBorracha()) {
            borracha.ativo = true
        }
    }

    camadaPincel.onmouseup = () => {
        pincel.ativo = false
        borracha.ativo = false
    }

    camadaPincel.onmousemove = (event) => {
        pincel.destino.x = event.clientX - camadaPincel.offsetLeft
        pincel.destino.y = event.clientY - camadaPincel.offsetTop
        pincel.movendo = true

        borracha.coordenadas.x = event.clientX - camadaPincel.offsetLeft
        borracha.coordenadas.y = event.clientY - camadaPincel.offsetTop
        borracha.movendo = true
    }
    
})
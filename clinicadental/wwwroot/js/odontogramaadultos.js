let procedimentosAdultos = [];
document.addEventListener('DOMContentLoaded', () => {
    const camada1 = document.querySelector('#camada1OdontogramaAdultos');
    const contexto1 = camada1.getContext('2d');
    const camada2 = document.querySelector('#camada2OdontogramaAdultos');
    const contexto2 = camada2.getContext('2d');
    const camada3 = document.querySelector('#camada3OdontogramaAdultos');
    const contexto3 = camada3.getContext('2d');

    let posicoesPadrao = {
        posicaoYInicialDente: 180,
        margemXEntreDentes: 8,
        margemYEntreDentes: 200
    };

    const tamanhoTelaReferencia = 1895;
    const alturaTelaReferencia = 512;

    let tamanhoColuna = camada1.width / 16;
    let tamanhoDente = tamanhoColuna - (2 * posicoesPadrao.margemXEntreDentes);

    let dimensoesTrapezio = {
        baseMaior: tamanhoDente,
        lateral: tamanhoDente / 4,
        baseMenor: (tamanhoDente / 4) * 3
    };

    let numeroDentes = {
        superior: ['18', '17', '16', '15', '14', '13', '12', '11', '21', '22', '23', '24', '25', '26', '27', '28'],
        inferior: ['48', '47', '46', '45', '44', '43', '42', '41', '31', '32', '33', '34', '35', '36', '37', '38']
    };

    let numeroDenteXOrdemExibicaoDente = new Array();
    let marcacoes = [];

    const itensProcedimento = [
        { nome: 'Lesión de Caries Activa', cor: '#FF0000', ide: 1 },        // Rojo
        { nome: 'Obturado y Cariado', cor: '#FFA500', ide: 2 },     // Naranja
        { nome: 'Obturado sin Caries', cor: '#800080', ide: 3 },           // Púrpura
        { nome: 'Perdido por Caries', cor: '#000000', ide: 4 }, // negro
        { nome: 'Perdido por otra Razón', cor: '#008000', ide: 5 },       // Verde
        { nome: 'Soporte de Puente Corono Especial o Funda', cor: '#FFFF00', ide: 6 },            // Amarillo
        { nome: 'No Erupcionado', cor: '#00FFFF', ide: 7 },          // Cian
        { nome: 'Traumatismos', cor: '#0000FF', ide: 8 },                 // Azul      
        { nome: 'No Registrado', cor: '#A52A2A', ide: 9 }                                // negro
    ];

    let denteSelecionado = null;

    const definePosicaoXInicialDente = (index) => {
        if (index === 0) return (index * tamanhoDente) + (posicoesPadrao.margemXEntreDentes * index) + posicoesPadrao.margemXEntreDentes;
        else return (index * tamanhoDente) + (2 * posicoesPadrao.margemXEntreDentes * index) + posicoesPadrao.margemXEntreDentes;
    };

    const desenharDente = (posicaoX, posicaoY) => {
        const contexto = contexto1;
        contexto.shadowColor = 'rgba(0, 0, 0, 0)';
        contexto.shadowBlur = 2;
        contexto.shadowOffsetX = 2;
        contexto.shadowOffsetY = 1;

        contexto.beginPath();
        contexto.moveTo(posicaoX, posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.lateral + posicaoY);
        contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.lateral + posicaoY);
        contexto.closePath();
        contexto.strokeStyle = '#000000';
        contexto.lineWidth = 0.7;
        contexto.stroke();

        contexto.beginPath();
        contexto.moveTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.lateral + posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
        contexto.closePath();
        contexto.stroke();

        contexto.beginPath();
        contexto.moveTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
        contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
        contexto.lineTo(posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
        contexto.closePath();
        contexto.stroke();

        contexto.beginPath();
        contexto.moveTo(posicaoX, posicaoY);
        contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.lateral + posicaoY);
        contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
        contexto.lineTo(posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
        contexto.closePath();
        contexto.stroke();

        contexto.shadowColor = 'rgba(0, 0, 0, 0)';
        contexto.shadowBlur = 0;
        contexto.shadowOffsetX = 0;
        contexto.shadowOffsetY = 0;
    };

    const definePosicaoXInicialQuadrado = (index) => {
        if (index === 0) return (index * tamanhoDente) + posicoesPadrao.margemXEntreDentes;
        else return (index * tamanhoDente) + (2 * index * posicoesPadrao.margemXEntreDentes);
    };

    const desenharQuadradoNumDente = (quadrado) => {
        let tamanhoFonte = (40 * (quadrado.primeiroOuUltimoDente ? quadrado.largura + posicoesPadrao.margemXEntreDentes : quadrado.largura)) / 118.4375;
        contexto1.font = `${tamanhoFonte}px arial`;
        contexto1.strokeStyle = '#000000';
        contexto1.fillStyle = '#000000';
        contexto1.strokeRect(quadrado.position.x, quadrado.position.y, quadrado.largura, quadrado.altura);
        contexto1.fillText(quadrado.numeroDente, quadrado.position.x + tamanhoDente / 2.8, quadrado.position.y + (tamanhoDente / 2.5));
    };

    const exibeProcedimentos = () => {
        contexto2.clearRect(0, 0, camada2.width, camada2.height);
        const procedimentosPorDenteFace = procedimentosAdultos.reduce((acc, proc) => {
            const key = `${proc.numeroDente}-${proc.faceDente}`;
            if (!acc[key]) acc[key] = [];
            acc[key].push(proc);
            return acc;
        }, {});

        Object.entries(procedimentosPorDenteFace).forEach(([key, procs]) => {
            const [numeroDente, faceDente] = key.split('-');
            const ordemExibicao = getOrdemExibicaoPorNumeroDente(numeroDente);
            const corProcedimento = procs.length === 1 ? itensProcedimento.find(item => item.nome === procs[0].nome).cor : '#000000';
            marcarSecao(contexto2, ordemExibicao, parseInt(faceDente), corProcedimento, true, procs.length > 1, procs);
        });
    };

    const marcarSecao = (contexto, ordemExibicaoDente, face, cor, fill = false, dividir = false, procedimentosAdultos = []) => {
        contexto.lineWidth = 1.3;
        let posicaoY = ordemExibicaoDente <= 16 ? posicoesPadrao.posicaoYInicialDente : dimensoesTrapezio.baseMaior + posicoesPadrao.margemYEntreDentes + posicoesPadrao.posicaoYInicialDente;
        let posicaoX = definePosicaoXInicialDente(ordemExibicaoDente - 1 - (ordemExibicaoDente > 16 ? 16 : 0));

        contexto.strokeStyle = '#d94141';

        if (!dividir || procedimentosAdultos.length <= 1) {
            contexto.fillStyle = cor;
            if (face === 1) {
                contexto.beginPath();
                contexto.moveTo(posicaoX, posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.lateral + posicaoY);
                contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.lateral + posicaoY);
                contexto.closePath();
                if (fill) contexto.fill();
                contexto.stroke();
            } else if (face === 2) {
                contexto.beginPath();
                contexto.moveTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.lateral + posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
                contexto.closePath();
                if (fill) contexto.fill();
                contexto.stroke();
            } else if (face === 3) {
                contexto.beginPath();
                contexto.moveTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMaior + posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
                contexto.lineTo(posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
                contexto.closePath();
                if (fill) contexto.fill();
                contexto.stroke();
            } else if (face === 4) {
                contexto.beginPath();
                contexto.moveTo(posicaoX, posicaoY);
                contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.lateral + posicaoY);
                contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
                contexto.lineTo(posicaoX, dimensoesTrapezio.baseMaior + posicaoY);
                contexto.closePath();
                if (fill) contexto.fill();
                contexto.stroke();
            } else if (face === 5) {
                contexto.beginPath();
                contexto.moveTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.lateral + posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.lateral + posicaoY);
                contexto.lineTo(dimensoesTrapezio.baseMenor + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
                contexto.lineTo(dimensoesTrapezio.lateral + posicaoX, dimensoesTrapezio.baseMenor + posicaoY);
                contexto.closePath();
                if (fill) contexto.fill();
                contexto.stroke();
            }
        } else {
            const numDivisoes = procedimentosAdultos.length;
            const larguraDivisao = (face === 5 ? dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral : dimensoesTrapezio.baseMaior) / numDivisoes;

            procedimentosAdultos.forEach((procedimento, index) => {
                contexto.fillStyle = itensProcedimento.find(item => item.nome === procedimento.nome).cor;
                const inicio = index * larguraDivisao;
                const fim = (index + 1) * larguraDivisao;

                if (face === 1) {
                    contexto.beginPath();
                    contexto.moveTo(posicaoX + inicio, posicaoY);
                    contexto.lineTo(posicaoX + fim, posicaoY);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) * (fim / dimensoesTrapezio.baseMaior), posicaoY + dimensoesTrapezio.lateral);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) * (inicio / dimensoesTrapezio.baseMaior), posicaoY + dimensoesTrapezio.lateral);
                    contexto.closePath();
                    contexto.fill();
                    contexto.stroke();
                } else if (face === 2) {
                    contexto.beginPath();
                    contexto.moveTo(posicaoX + dimensoesTrapezio.baseMaior, posicaoY + inicio);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.baseMenor, posicaoY + dimensoesTrapezio.lateral + inicio * (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) / dimensoesTrapezio.baseMaior);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.baseMenor, posicaoY + dimensoesTrapezio.lateral + fim * (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) / dimensoesTrapezio.baseMaior);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.baseMaior, posicaoY + fim);
                    contexto.closePath();
                    contexto.fill();
                    contexto.stroke();
                } else if (face === 3) {
                    contexto.beginPath();
                    contexto.moveTo(posicaoX + inicio, posicaoY + dimensoesTrapezio.baseMaior);
                    contexto.lineTo(posicaoX + fim, posicaoY + dimensoesTrapezio.baseMaior);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) * (fim / dimensoesTrapezio.baseMaior), posicaoY + dimensoesTrapezio.baseMenor);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) * (inicio / dimensoesTrapezio.baseMaior), posicaoY + dimensoesTrapezio.baseMenor);
                    contexto.closePath();
                    contexto.fill();
                    contexto.stroke();
                } else if (face === 4) {
                    contexto.beginPath();
                    contexto.moveTo(posicaoX, posicaoY + inicio);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral, posicaoY + dimensoesTrapezio.lateral + inicio * (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) / dimensoesTrapezio.baseMaior);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral, posicaoY + dimensoesTrapezio.lateral + fim * (dimensoesTrapezio.baseMenor - dimensoesTrapezio.lateral) / dimensoesTrapezio.baseMaior);
                    contexto.lineTo(posicaoX, posicaoY + fim);
                    contexto.closePath();
                    contexto.fill();
                    contexto.stroke();
                } else if (face === 5) {
                    contexto.beginPath();
                    contexto.moveTo(posicaoX + dimensoesTrapezio.lateral + inicio, posicaoY + dimensoesTrapezio.lateral);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + fim, posicaoY + dimensoesTrapezio.lateral);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + fim, posicaoY + dimensoesTrapezio.baseMenor);
                    contexto.lineTo(posicaoX + dimensoesTrapezio.lateral + inicio, posicaoY + dimensoesTrapezio.baseMenor);
                    contexto.closePath();
                    contexto.fill();
                    contexto.stroke();
                }
            });
        }
    };

    const exibirEstrutura = () => {
        contexto1.fillStyle = '#FFFFFF';
        contexto1.fillRect(0, 0, camada1.width, camada1.height);

        for (let index = 0; index < 16; index++) {
            const posicaoX = definePosicaoXInicialDente(index);
            desenharDente(posicaoX, posicoesPadrao.posicaoYInicialDente);
            desenharDente(posicaoX, posicoesPadrao.margemYEntreDentes + tamanhoDente + posicoesPadrao.posicaoYInicialDente);
        }

        numeroDentes.superior.forEach((numero, index) => {
            const posicaoX = definePosicaoXInicialQuadrado(index);
            desenharQuadradoNumDente({
                position: { x: posicaoX, y: (posicoesPadrao.margemYEntreDentes / 5) + tamanhoDente + posicoesPadrao.posicaoYInicialDente },
                primeiroOuUltimoDente: index === 0 || index === 15,
                numeroDente: numero,
                altura: tamanhoDente / 1.8,
                largura: index === 0 || index === 15 ? tamanhoDente + posicoesPadrao.margemXEntreDentes : tamanhoDente + 2 * posicoesPadrao.margemXEntreDentes
            });
        });

        numeroDentes.inferior.forEach((numero, index) => {
            const posicaoX = definePosicaoXInicialQuadrado(index);
            desenharQuadradoNumDente({
                position: { x: posicaoX, y: (posicoesPadrao.margemYEntreDentes / 5) + (tamanhoDente / 1.8) + tamanhoDente + posicoesPadrao.posicaoYInicialDente },
                primeiroOuUltimoDente: index === 0 || index === 15,
                numeroDente: numero,
                altura: tamanhoDente / 1.8,
                largura: index === 0 || index === 15 ? tamanhoDente + posicoesPadrao.margemXEntreDentes : tamanhoDente + 2 * posicoesPadrao.margemXEntreDentes
            });
        });
    };

    const getInfoDentePosicaoAtual = (x, y) => {
        let numeroDente = null, faceDente = null, indice = null;

        if (y >= posicoesPadrao.posicaoYInicialDente && y <= posicoesPadrao.posicaoYInicialDente + tamanhoDente) {
            indice = Math.floor((x - posicoesPadrao.margemXEntreDentes) / (tamanhoDente + 2 * posicoesPadrao.margemXEntreDentes));
            if (indice >= 0 && indice < 16) numeroDente = numeroDentes.superior[indice];
        } else if (y >= (tamanhoDente + posicoesPadrao.margemYEntreDentes + posicoesPadrao.posicaoYInicialDente) && y <= (2 * tamanhoDente + posicoesPadrao.margemYEntreDentes + posicoesPadrao.posicaoYInicialDente)) {
            indice = Math.floor((x - posicoesPadrao.margemXEntreDentes) / (tamanhoDente + 2 * posicoesPadrao.margemXEntreDentes));
            if (indice >= 0 && indice < 16) numeroDente = numeroDentes.inferior[indice];
        }

        if (numeroDente) {
            let px = x - definePosicaoXInicialDente(indice);
            let py = y - (numeroDente.startsWith('1') || numeroDente.startsWith('2') ? posicoesPadrao.posicaoYInicialDente : posicoesPadrao.posicaoYInicialDente + posicoesPadrao.margemYEntreDentes + tamanhoDente);

            if (py > 0 && py < (tamanhoDente / 4) && px > py && py < tamanhoDente - px) faceDente = 1;
            else if (px > (tamanhoDente / 4) * 3 && px < tamanhoDente && py < px && tamanhoDente - px < py) faceDente = 2;
            else if (py > (tamanhoDente / 4) * 3 && py < tamanhoDente && px < py && px > tamanhoDente - py) faceDente = 3;
            else if (px > 0 && px < (tamanhoDente / 4) && py > px && px < tamanhoDente - py) faceDente = 4;
            else if (px > (tamanhoDente / 4) && px < (tamanhoDente / 4) * 3 && py > (tamanhoDente / 4) && py < (tamanhoDente / 4) * 3) faceDente = 5;
        }

        return { numeroDente, faceDente };
    };

    const exibeMarcacoes = () => {
        contexto2.clearRect(0, 0, camada2.width, camada2.height);
        exibeProcedimentos();
        marcacoes.forEach(({ numeroDente, faceDente }) => {
            marcarSecao(contexto2, getOrdemExibicaoPorNumeroDente(numeroDente), faceDente, '#ff0000', false);
        });
    };

    const resizeCanvas = () => {
        let novoWidth = window.innerWidth - 500;

        // Definir límites mínimo y máximo
        const minWidth = 500; // mínimo
        const maxWidth = 940; // máximo

        // Aplicar los límites
        novoWidth = Math.max(minWidth, Math.min(novoWidth, maxWidth));

        camada1.width = camada2.width = camada3.width = novoWidth;

        const altura = (novoWidth * alturaTelaReferencia) / tamanhoTelaReferencia;
        camada1.height = camada2.height = camada3.height = altura;

        posicoesPadrao.margemXEntreDentes = (novoWidth * 8) / tamanhoTelaReferencia;
        posicoesPadrao.margemYEntreDentes = (novoWidth * 200) / tamanhoTelaReferencia;
        posicoesPadrao.posicaoYInicialDente = (novoWidth * 50) / tamanhoTelaReferencia;

        tamanhoColuna = novoWidth / 16;
        tamanhoDente = tamanhoColuna - (2 * posicoesPadrao.margemXEntreDentes);

        dimensoesTrapezio = {
            baseMaior: tamanhoDente,
            lateral: tamanhoDente / 4,
            baseMenor: (tamanhoDente / 4) * 3
        };

        exibirEstrutura();
        exibeMarcacoes();
    };


    const getOrdemExibicaoPorNumeroDente = (numero) => {
        return numeroDenteXOrdemExibicaoDente[numero] + 1;
    };

    const atualizaTabela = () => {
        const tbody = document.getElementById('bodyProcedimentosAdultos'); // Actualizado
        let trs = '';

        procedimentosAdultos.forEach((item, index) => {
            let nomeCara;
            const numeroDente = parseInt(item.numeroDente);
            const ehDenteSuperior = numeroDente >= 11 && numeroDente <= 28;
            const ehDenteInferior = numeroDente >= 31 && numeroDente <= 48;

            if (ehDenteSuperior) {
                switch (item.faceDente) {
                    case 1: nomeCara = 'Vestibular'; break;
                    case 3: nomeCara = 'Lingual'; break;
                    case 2: nomeCara = (numeroDente >= 11 && numeroDente <= 18) ? 'Mesial' : 'Distal'; break;
                    case 4: nomeCara = (numeroDente >= 11 && numeroDente <= 18) ? 'Distal' : 'Mesial'; break;
                    case 5: nomeCara = 'Oclusal'; break;
                    default: nomeCara = item.faceDente;
                }
            } else if (ehDenteInferior) {
                switch (item.faceDente) {
                    case 1: nomeCara = 'Lingual'; break;
                    case 3: nomeCara = 'Vestibular'; break;
                    case 2: nomeCara = (numeroDente >= 41 && numeroDente <= 48) ? 'Mesial' : 'Distal'; break;
                    case 4: nomeCara = (numeroDente >= 41 && numeroDente <= 48) ? 'Distal' : 'Mesial'; break;
                    case 5: nomeCara = 'Oclusal'; break;
                    default: nomeCara = item.faceDente;
                }
            } else {
                nomeCara = item.faceDente;
            }

            trs += `
                <tr>
                    <td>${item.numeroDente}</td>
                    <td>${nomeCara}</td>
                    <td>${item.nome}</td>
                    <td style="display: none">${item.ide}</td>
                    <td><button class="btn btn-danger btn-sm" data-index="${index}"><i class="bi bi-trash-fill fs-5"></i></button></td>
                </tr>
            `;
        });

        tbody.innerHTML = trs;

        const botoesApagar = tbody.querySelectorAll('.btn-danger');
        botoesApagar.forEach(botao => {
            botao.addEventListener('click', (e) => {
                const index = parseInt(e.target.closest('button').getAttribute('data-index'));
                window.apagarProcedimentoAdultos(index); // Cambiado a apagarProcedimentoAdultos
            });
        });
    };

    window.apagarProcedimentoAdultos = (index) => { // Cambiado a apagarProcedimentoAdultos
        const proc = procedimentosAdultos[index];
        marcacoes = marcacoes.filter(m => !(m.numeroDente === proc.numeroDente && m.faceDente === proc.faceDente));
        procedimentosAdultos.splice(index, 1);
        exibeMarcacoes();
        atualizaTabela();
    };

    const iniciaOdontograma = () => {
        numeroDentes.superior.forEach((numero, index) => numeroDenteXOrdemExibicaoDente[numero] = index);
        numeroDentes.inferior.forEach((numero, index) => numeroDenteXOrdemExibicaoDente[numero] = index + 16);

        resizeCanvas();

        const selectAfeccoes = document.getElementById('nomeProcedimentoAdultos');
        selectAfeccoes.innerHTML = '<option value="">-- Seleccione una afección --</option>' +
            itensProcedimento.map(item => `<option value="${item.nome}">${item.nome}</option>`).join('');

        camada3.onmousemove = (event) => {
            const rect = camada3.getBoundingClientRect();
            const x = event.clientX - rect.left;
            const y = event.clientY - rect.top;
            const { numeroDente, faceDente } = getInfoDentePosicaoAtual(x, y);

            contexto3.clearRect(0, 0, camada3.width, camada3.height);
            if (numeroDente && faceDente) {
                marcarSecao(contexto3, getOrdemExibicaoPorNumeroDente(numeroDente), faceDente, '#ff9d00', false);
            }
        };

        camada3.onclick = (event) => {
            const rect = camada3.getBoundingClientRect();
            const x = event.clientX - rect.left;
            const y = event.clientY - rect.top;
            denteSelecionado = getInfoDentePosicaoAtual(x, y);
            if (denteSelecionado.numeroDente && denteSelecionado.faceDente) {
                const jaMarcadoIndex = marcacoes.findIndex(m => m.numeroDente === denteSelecionado.numeroDente && m.faceDente === denteSelecionado.faceDente);
                if (jaMarcadoIndex === -1) {
                    marcacoes.push(denteSelecionado);
                } else {
                    marcacoes.splice(jaMarcadoIndex, 1);
                }
                exibeMarcacoes();
            }
        };

        document.getElementById('botaoAdicionarAdultos').onclick = () => {
            if (marcacoes.length > 0) {
                const nomeProcedimento = document.getElementById('nomeProcedimentoAdultos').value;
                const procedimentoSelecionado = itensProcedimento.find(item => item.nome === nomeProcedimento);
                if (nomeProcedimento) {
                    marcacoes.forEach(marcacao => {
                        procedimentosAdultos.push({
                            numeroDente: marcacao.numeroDente,
                            faceDente: marcacao.faceDente,
                            nome: nomeProcedimento,
                            ide: procedimentoSelecionado.ide // Agregar el ide
                        });
                    });
                    marcacoes = [];
                    exibeMarcacoes();
                    exibeProcedimentos();
                    atualizaTabela();
                    denteSelecionado = null;
                } else {
                    alert('Por favor, seleccione una afección.');
                }
            } else {
                alert('Por favor, seleccione al menos una cara de un diente primero.');
            }
        };

        // Manejar el envío del formulario de adultos
        document.getElementById('odontogramaFormAdultos').addEventListener('submit', function (e) {
            const procedimentosFormatted = procedimentosAdultos.map(proc => ({
                NroPiezaDental: proc.numeroDente,
                CaraPiezaDental: mapNumberToCara(proc.faceDente, proc.numeroDente),
                IdAfeccion: proc.ide,
                IdHistorialClinico: window.idHistorialClinico
            }));
            document.getElementById('procedimientosInputAdultos').value = JSON.stringify(procedimentosFormatted);
            console.log("Datos enviados desde Adultos:", JSON.stringify(procedimentosFormatted)); 
        });

        exibeProcedimentos();
        atualizaTabela();
    };

    window.cargarDatosOdontograma = (data) => {
        if (data && data.length > 0) {
            data.forEach(function (item) {
                const numeroDente = parseInt(item.nroPiezaDental);
                // Filtrar solo dientes de adultos (11-48)
                if (numeroDente >= 11 && numeroDente <= 48) {
                    var afeccion = itensProcedimento.find(proc => proc.ide === item.idAfeccion);
                    var faceDente = mapCaraToNumber(item.caraPiezaDental); // Convertir a número
                    procedimentosAdultos.push({
                        numeroDente: item.nroPiezaDental,
                        faceDente: faceDente,
                        nome: afeccion ? afeccion.nome : "No Registrado",
                        ide: item.idAfeccion
                    });
                }
            });
            exibeProcedimentos(); // Pintar el odontograma
            atualizaTabela();     // Llenar la tabla
        }
    };

    function mapCaraToNumber(cara) {
        switch (cara.toLowerCase()) {
            case 'vestibular': return 1;
            case 'mesial': return 2;
            case 'lingual': return 3;
            case 'distal': return 4;
            case 'oclusal': return 5;
            default: return 0; // Valor por defecto si no coincide
        }
    }
    
    // Función para mapear el número de la cara al nombre esperado por el backend
    function mapNumberToCara(faceDente, numeroDente) {
        const ehDenteSuperior = numeroDente >= 11 && numeroDente <= 28;
        const ehDenteInferior = numeroDente >= 31 && numeroDente <= 48;

        if (ehDenteSuperior) {
            switch (faceDente) {
                case 1: return 'Vestibular';
                case 2: return (numeroDente >= 11 && numeroDente <= 18) ? 'Mesial' : 'Distal';
                case 3: return 'Lingual';
                case 4: return (numeroDente >= 11 && numeroDente <= 18) ? 'Distal' : 'Mesial';
                case 5: return 'Oclusal';
                default: return '';
            }
        } else if (ehDenteInferior) {
            switch (faceDente) {
                case 1: return 'Lingual';
                case 2: return (numeroDente >= 41 && numeroDente <= 48) ? 'Mesial' : 'Distal';
                case 3: return 'Vestibular';
                case 4: return (numeroDente >= 41 && numeroDente <= 48) ? 'Distal' : 'Mesial';
                case 5: return 'Oclusal';
                default: return '';
            }
        }
        return '';
    }

    window.addEventListener("resize", resizeCanvas);
    iniciaOdontograma();

});
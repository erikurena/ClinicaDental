let procedimentosAdultos = [];

document.addEventListener('DOMContentLoaded', () => {
    const camada1 = document.querySelector('#camada1OdontogramaAdultos');
    const contexto1 = camada1.getContext('2d');
    const camada2 = document.querySelector('#camada2OdontogramaAdultos');
    const contexto2 = camada2.getContext('2d');

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

    const itensProcedimento = [
        { nome: 'Lesión de Caries Activa', cor: '#FF0000', ide: 1 },
        { nome: 'Obturado y Cariado', cor: '#FFA500', ide: 2 },
        { nome: 'Obturado sin Caries', cor: '#800080', ide: 3 },
        { nome: 'Perdido por Caries', cor: '#000000', ide: 4 },
        { nome: 'Perdido por otra Razón', cor: '#008000', ide: 5 },
        { nome: 'Soporte de Puente Corono Especial o Funda', cor: '#FFFF00', ide: 6 },
        { nome: 'No Erupcionado', cor: '#00FFFF', ide: 7 },
        { nome: 'Traumatismos', cor: '#0000FF', ide: 8 },
        { nome: 'No Registrado', cor: '#A52A2A', ide: 9 }
    ];

    const definePosicaoXInicialDente = (index) => {
        if (index === 0) return (index * tamanhoDente) + (posicoesPadrao.margemXEntreDentes * index) + posicoesPadrao.margemXEntreDentes;
        else return (index * tamanhoDente) + (2 * posicoesPadrao.margemXEntreDentes * index) + posicoesPadrao.margemXEntreDentes;
    };

    const desenharDente = (posicaoX, posicaoY) => {
        const contexto = contexto1;
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

    const resizeCanvas = () => {
        camada1.width = camada2.width = window.innerWidth - 500;
        const altura = (camada1.width * alturaTelaReferencia) / tamanhoTelaReferencia;
        camada1.height = camada2.height = altura;

        posicoesPadrao.margemXEntreDentes = (camada1.width * 8) / tamanhoTelaReferencia;
        posicoesPadrao.margemYEntreDentes = (camada1.width * 200) / tamanhoTelaReferencia;
        posicoesPadrao.posicaoYInicialDente = (camada1.width * 50) / tamanhoTelaReferencia;

        tamanhoColuna = camada1.width / 16;
        tamanhoDente = tamanhoColuna - (2 * posicoesPadrao.margemXEntreDentes);

        dimensoesTrapezio = {
            baseMaior: tamanhoDente,
            lateral: tamanhoDente / 4,
            baseMenor: (tamanhoDente / 4) * 3
        };

        exibirEstrutura();
        exibeProcedimentos();
    };

    const getOrdemExibicaoPorNumeroDente = (numero) => {
        return numeroDenteXOrdemExibicaoDente[numero] + 1;
    };

    const atualizaTabela = () => {
        const tbody = document.getElementById('bodyProcedimentosAdultos');
        let trs = '';

        procedimentosAdultos.forEach((item) => {
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
                </tr>
            `;
        });

        tbody.innerHTML = trs;
    };

    const iniciaOdontograma = () => {
        numeroDentes.superior.forEach((numero, index) => numeroDenteXOrdemExibicaoDente[numero] = index);
        numeroDentes.inferior.forEach((numero, index) => numeroDenteXOrdemExibicaoDente[numero] = index + 16);

        resizeCanvas();
        exibeProcedimentos();
        atualizaTabela();
    };

    window.cargarDatosOdontograma = (data) => {
        if (data && data.length > 0) {
            data.forEach(function (item) {
                const numeroDente = parseInt(item.nroPiezaDental);
                if (numeroDente >= 11 && numeroDente <= 48) {
                    var afeccion = itensProcedimento.find(proc => proc.ide === item.idAfeccion);
                    var faceDente = mapCaraToNumber(item.caraPiezaDental);
                    procedimentosAdultos.push({
                        numeroDente: item.nroPiezaDental,
                        faceDente: faceDente,
                        nome: afeccion ? afeccion.nome : "No Registrado",
                        ide: item.idAfeccion
                    });
                }
            });
            exibeProcedimentos();
            atualizaTabela();
        }
    };

    function mapCaraToNumber(cara) {
        switch (cara.toLowerCase()) {
            case 'vestibular': return 1;
            case 'mesial': return 2;
            case 'lingual': return 3;
            case 'distal': return 4;
            case 'oclusal': return 5;
            default: return 0;
        }
    }

    window.addEventListener("resize", resizeCanvas);
    iniciaOdontograma();
});
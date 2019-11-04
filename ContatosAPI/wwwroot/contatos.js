
//const uri = "https://localhost:44308/api/contato";
const uri = "api/contato";
let contatos = null;

function labelCount(count) {
    const el = $("#counter");
    switch (count) {
        case 0:
            el.text(`Nenhum ${label}`);
            break;
        case 1:
            el.text(`${count} contato`);
            break;
        default:
            el.text(`${count} contatos`);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: "GET",
        url: uri,
        cache: false,
        success: function (data) {
            const tBody = $("#contatos");

            $(tBody).empty();

            labelCount(data.length);

            $.each(data, function (key, contato) {
                const tr = $("<tr></tr>")
                    .append($("<td></td>").text(contato.nome))
                    .append($("<td></td>").text(contato.telefone))
                    .append($("<td></td>").text(contato.aniversario))
                    .append(
                        $("<td></td>").append(
                            $("<button>Editar</button>").on("click", function () {
                                editContato(contato.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button class='button-delete'>Deletar</button>").on("click", function () {
                                deleteContato(contato.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            contatos = data;

        }
    });
}

function addContato() {
    const contato = contatoGet("add");
    if (!contatoValida(contato)) return;

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: uri,
        contentType: "application/json",
        data: JSON.stringify(contato),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(`Erro ao criar contato: ${textStatus}`);
        },
        success: function (result) {
            getData();
            $("#add-nome").val("");
            $("#add-telefone").val("");
            $("#add-aniversario").val("");
        }
    });
}

function deleteContato(id) {
    $.ajax({
        url: uri + "/" + id,
        type: "DELETE",
        success: function (result) {
            getData();
        }
    });
}

function editContato(id) {
    $.each(contatos, function (key, contato) {
        if (contato.id === id) {
            $("#edit-nome").val(contato.nome);
            $("#edit-telefone").val(contato.telefone);
            $("#edit-aniversario").val(contato.aniversario);
            $("#edit-id").val(contato.id);
        }
    });
    $("#editContato").css({ display: "block" });
    $("#addContato").css({ display: "none" });
}

$(".editForm").on("submit", function () {
    const contato = contatoGet("edit");
    if (!contatoValida(contato)) return;

    $.ajax({
        url: uri + "/" + contato.id,
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(contato),
        error: function (jqXHR, textStatus, errorThrown) {
            alert(`Erro ao salvar alteração: ${errorThrown}`);
        },
        success: function (result) {
            alert("Contato alterado com sucesso");
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $("#editContato").css({ display: "none" });
    $("#addContato").css({ display: "block" });
}

function contatoGet(modo) {
    const cNome = `#${modo}-nome`;
    const cTelefone = `#${modo}-telefone`;
    const cAniversario = `#${modo}-aniversario`;
    const cId = `#${modo}-id`;
    return {
        campoNome: cNome,
        campoTelefone: cTelefone,
        campoAniversario: cAniversario,
        campoId: cId,

        nome: $(cNome).val(),
        telefone: $(cTelefone).val(),
        aniversario: $(cAniversario).val(),
        id: $(cId).val()
    };
}

function contatoValida(contato) {
    let erro = "";
    let foco = "";
    if (contato.nome.length < 5 || contato.nome.length > 20) {
        erro += "\nNome deve ter entre 5 e 20 caracteres.";
        foco = contato.campoNome;
    }

    const regexFone = new RegExp("^[(]{0,1}[0-9]{1,2}[)]{0,1}[-\s\./0-9]*$");
    if (contato.telefone.length < 8 || contato.telefone.length > 15 || !regexFone.test(contato.telefone)) {
        erro += "\nTelefone com formato inválido.";
        foco = foco === "" ? contato.campoTelefone : foco;
    }

    const regexData = new RegExp("^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))$");
    if (!regexData.test(contato.aniversario)) {
        erro += "\nAniversário deve estar no formato dd/mm.";
        foco = foco === "" ? contato.campoAniversario : foco;
    }

    if (erro === "") return true;

    alert(`Contato inválido\n${erro}`);
    $(foco).focus();
    return false;
}
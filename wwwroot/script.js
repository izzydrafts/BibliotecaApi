const apiUrl = "http://localhost:5057/api/Livros";

let livroEditandoId = null;

function mostrarSecao(secaoId) {
    document.querySelectorAll(".secao").forEach(secao => {
        secao.classList.remove("ativa");
    });

    const secaoEscolhida = document.getElementById(secaoId);

    secaoEscolhida.classList.add("ativa");

    secaoEscolhida.scrollIntoView({
        behavior: "smooth",
        block: "start"
    });
}
async function carregarLivros() {

    const resposta = await fetch(apiUrl);

    const livros = await resposta.json();

    const lista = document.getElementById("listaLivros");

    lista.innerHTML = "";

    livros.forEach(livro => {

        const item = document.createElement("li");

       item.innerHTML = `
        <h3>${livro.titulo}</h3>

        <p><strong>Autor:</strong> ${livro.autor}</p>

        <p><strong>Gênero:</strong> ${livro.genero}</p>

        <p><strong>Quantidade:</strong> ${livro.quantidade}</p>

        <div class="acoes">

       <button class="btn-editar">
            Editar
        </button>

        <button class="btn-excluir"
            onclick="excluirLivro('${livro.id}')">
            Excluir
        </button>

        </div>
        `;

        item.querySelector(".btn-editar")
        .addEventListener("click", () => editarLivro(livro));
        lista.appendChild(item);
    });
}

async function cadastrarLivro() {

    const livro = {
        titulo: document.getElementById("titulo").value,
        autor: document.getElementById("autor").value,
        genero: document.getElementById("genero").value,
        quantidade: parseInt(document.getElementById("quantidade").value)
    };

    console.log("ID EDITANDO:", livroEditandoId);
    
    if (livroEditandoId) {

        const resposta = await fetch(`${apiUrl}/${livroEditandoId}`, {
        method: "PUT",
        headers: {
        "Content-Type": "application/json"
        },

        body: JSON.stringify({
            id: livroEditandoId,
            titulo: livro.titulo,
            autor: livro.autor,
            genero: livro.genero,
            quantidade: livro.quantidade
        })
});

console.log(resposta.status);

        livroEditandoId = null;

    } else {

        await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(livro)
        });
    }

    limparCampos();

    carregarLivros();
    
    document.getElementById("listaLivros")
    .scrollIntoView({
    behavior: "smooth"
    });
}

function editarLivro(livro) {

    livroEditandoId = livro.id;

    console.log("ID EDITANDO:", livroEditandoId);

    document.getElementById("titulo").value = livro.titulo;

    document.getElementById("autor").value = livro.autor;

    document.getElementById("genero").value = livro.genero;

    document.getElementById("quantidade").value = livro.quantidade;

    document.getElementById("tituloFormulario")
    .innerText = "Editando Livro";

    window.scrollTo({
    top: 0,
    behavior: "smooth"
    });
}

function limparCampos() {

    document.getElementById("titulo").value = "";

    document.getElementById("autor").value = "";

    document.getElementById("genero").value = "";

    document.getElementById("quantidade").value = "";

    document.getElementById("tituloFormulario")
    .innerText = "Cadastrar Livro";
}

async function excluirLivro(id) {

    await fetch(`${apiUrl}/${id}`, {
        method: "DELETE"
    });

    carregarLivros();
}
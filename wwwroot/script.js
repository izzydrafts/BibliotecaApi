const apiUrl = "http://localhost:5057/api/Livros";

async function carregarLivros() {

    const resposta = await fetch(apiUrl);

    const livros = await resposta.json();

    const lista = document.getElementById("listaLivros");

    lista.innerHTML = "";

    livros.forEach(livro => {

        const item = document.createElement("li");

        item.innerHTML = `
            <strong>${livro.titulo}</strong>
            - ${livro.autor}
            - ${livro.genero}
            - Quantidade: ${livro.quantidade}
        `;

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

    await fetch(apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(livro)
    });

    carregarLivros();

    document.getElementById("titulo").value = "";
    document.getElementById("autor").value = "";
    document.getElementById("genero").value = "";
    document.getElementById("quantidade").value = "";
}

carregarLivros();
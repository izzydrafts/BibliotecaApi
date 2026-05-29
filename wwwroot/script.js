const apiUrl = "http://localhost:5057/api/Livros";
const apiEmprestimosUrl = "http://localhost:5057/api/Emprestimos";

let livroEditandoId = null;
let emprestimoEditandoId = null;

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

    if (secaoId === "livros") {
        carregarLivros();
    }

    if (secaoId === "emprestimos") {
        carregarLivrosNoSelect();
        carregarEmprestimos();
    }
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
            <p><strong>ID:</strong> ${livro.id}</p>

            <div class="acoes">
                <button class="btn-editar">Editar</button>
                <button class="btn-excluir" onclick="excluirLivro('${livro.id}')">Excluir</button>
            </div>
        `;

        item.querySelector(".btn-editar")
            .addEventListener("click", () => editarLivro(livro));

        lista.appendChild(item);
    });
}

async function cadastrarLivro() {
    const livro = {
        titulo: document.getElementById("titulo").value.trim(),
        autor: document.getElementById("autor").value.trim(),
        genero: document.getElementById("genero").value.trim(),
        quantidade: parseInt(document.getElementById("quantidade").value)
    };

    if (!livro.titulo || !livro.autor || !livro.genero || isNaN(livro.quantidade) || livro.quantidade < 0) {
        alert("Preencha todos os campos do livro corretamente.");
        return;
    }

    if (livroEditandoId) {
        await fetch(`${apiUrl}/${livroEditandoId}`, {
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

    limparCamposLivro();
    carregarLivros();
    carregarLivrosNoSelect();

    document.getElementById("listaLivros").scrollIntoView({
        behavior: "smooth"
    });
}

function editarLivro(livro) {
    livroEditandoId = livro.id;

    document.getElementById("titulo").value = livro.titulo;
    document.getElementById("autor").value = livro.autor;
    document.getElementById("genero").value = livro.genero;
    document.getElementById("quantidade").value = livro.quantidade;

    document.getElementById("tituloFormulario").innerText = "Editando Livro";
    document.getElementById("btnSalvarLivro").innerText = "Salvar Alterações";

    document.getElementById("livros").scrollIntoView({
        behavior: "smooth"
    });
}

function limparCamposLivro() {
    document.getElementById("titulo").value = "";
    document.getElementById("autor").value = "";
    document.getElementById("genero").value = "";
    document.getElementById("quantidade").value = "";

    document.getElementById("tituloFormulario").innerText = "Cadastrar Livro";
    document.getElementById("btnSalvarLivro").innerText = "Cadastrar Livro";
}

async function excluirLivro(id) {
    await fetch(`${apiUrl}/${id}`, {
        method: "DELETE"
    });

    carregarLivros();
    carregarLivrosNoSelect();
}

async function carregarLivrosNoSelect() {
    const resposta = await fetch(apiUrl);
    const livros = await resposta.json();

    const select = document.getElementById("livroIdEmprestimo");

    if (!select) {
        return;
    }

    select.innerHTML = `<option value="">Selecione um livro</option>`;

    livros.forEach(livro => {
        const option = document.createElement("option");

        option.value = livro.id;
        option.textContent = `${livro.titulo} - ${livro.autor}`;

        select.appendChild(option);
    });
}

async function carregarEmprestimos() {
    const resposta = await fetch(apiEmprestimosUrl);
    const emprestimos = await resposta.json();

    const lista = document.getElementById("listaEmprestimos");
    lista.innerHTML = "";

    emprestimos.forEach(emprestimo => {
        const item = document.createElement("li");
        const nomeLivro = buscarNomeLivroNoSelect(emprestimo.livroId);

        item.innerHTML = `
            <h3>${emprestimo.nomeUsuario}</h3>
            <p><strong>Livro:</strong> ${nomeLivro}</p>
            <p><strong>ID do Livro:</strong> ${emprestimo.livroId}</p>
            <p><strong>Data do Empréstimo:</strong> ${new Date(emprestimo.dataEmprestimo).toLocaleDateString("pt-BR")}</p>
            <p><strong>Data de Devolução:</strong> ${new Date(emprestimo.dataDevolucao).toLocaleDateString("pt-BR")}</p>

            <div class="acoes">
                <button class="btn-editar">Editar</button>
                <button class="btn-excluir" onclick="excluirEmprestimo('${emprestimo.id}')">Excluir</button>
            </div>
        `;

        item.querySelector(".btn-editar")
            .addEventListener("click", () => editarEmprestimo(emprestimo));

        lista.appendChild(item);
    });
}

function buscarNomeLivroNoSelect(livroId) {
    const select = document.getElementById("livroIdEmprestimo");

    if (!select) {
        return livroId;
    }

    const option = Array.from(select.options).find(opcao => opcao.value === livroId);

    return option ? option.textContent : livroId;
}

async function cadastrarEmprestimo() {
    const emprestimo = {
        nomeUsuario: document.getElementById("nomeUsuarioEmprestimo").value.trim(),
        livroId: document.getElementById("livroIdEmprestimo").value,
        dataEmprestimo: document.getElementById("dataEmprestimo").value,
        dataDevolucao: document.getElementById("dataDevolucao").value
    };

    if (!emprestimo.nomeUsuario || !emprestimo.livroId || !emprestimo.dataEmprestimo || !emprestimo.dataDevolucao) {
        alert("Preencha todos os campos do empréstimo corretamente.");
        return;
    }

    if (emprestimoEditandoId) {
        await fetch(`${apiEmprestimosUrl}/${emprestimoEditandoId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                id: emprestimoEditandoId,
                nomeUsuario: emprestimo.nomeUsuario,
                livroId: emprestimo.livroId,
                dataEmprestimo: emprestimo.dataEmprestimo,
                dataDevolucao: emprestimo.dataDevolucao
            })
        });

        emprestimoEditandoId = null;
    } else {
        const quantidadeAtualizada = await atualizarQuantidadeLivro(emprestimo.livroId, -1);

        if (!quantidadeAtualizada) {
            return;
        }
        await fetch(apiEmprestimosUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(emprestimo)
        });
    }

    limparCamposEmprestimo();
    carregarEmprestimos();

    document.getElementById("listaEmprestimos").scrollIntoView({
        behavior: "smooth"
    });
}

function editarEmprestimo(emprestimo) {
    emprestimoEditandoId = emprestimo.id;

    document.getElementById("nomeUsuarioEmprestimo").value = emprestimo.nomeUsuario;
    document.getElementById("livroIdEmprestimo").value = emprestimo.livroId;
    document.getElementById("dataEmprestimo").value = emprestimo.dataEmprestimo.substring(0, 10);
    document.getElementById("dataDevolucao").value = emprestimo.dataDevolucao.substring(0, 10);

    document.getElementById("tituloFormularioEmprestimo").innerText = "Editando Empréstimo";
    document.getElementById("btnSalvarEmprestimo").innerText = "Salvar Alterações";

    document.getElementById("emprestimos").scrollIntoView({
        behavior: "smooth"
    });
}

async function excluirEmprestimo(id) {
    const resposta = await fetch(`${apiEmprestimosUrl}/${id}`);
    const emprestimo = await resposta.json();

    await fetch(`${apiEmprestimosUrl}/${id}`, {
        method: "DELETE"
    });

    await atualizarQuantidadeLivro(emprestimo.livroId, 1);

    carregarEmprestimos();
    carregarLivros();
    carregarLivrosNoSelect();
}

function limparCamposEmprestimo() {
    document.getElementById("nomeUsuarioEmprestimo").value = "";
    document.getElementById("livroIdEmprestimo").value = "";
    document.getElementById("dataEmprestimo").value = "";
    document.getElementById("dataDevolucao").value = "";

    document.getElementById("tituloFormularioEmprestimo").innerText = "Registrar Empréstimo";
    document.getElementById("btnSalvarEmprestimo").innerText = "Registrar Empréstimo";
}
async function atualizarQuantidadeLivro(livroId, alteracao) {
    const resposta = await fetch(`${apiUrl}/${livroId}`);
    const livro = await resposta.json();

    const novaQuantidade = livro.quantidade + alteracao;

    if (novaQuantidade < 0) {
        alert("Este livro não possui quantidade disponível para empréstimo.");
        return false;
    }

    await fetch(`${apiUrl}/${livroId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            id: livro.id,
            titulo: livro.titulo,
            autor: livro.autor,
            genero: livro.genero,
            quantidade: novaQuantidade
        })
    });

    return true;
}
carregarLivros();
carregarLivrosNoSelect();
carregarEmprestimos();
document.getElementById('employee-form').addEventListener('submit', function(event) {
    event.preventDefault(); // Evita o envio padrão do formulário

    // Cria um FormData para enviar os dados do formulário
    const formData = new FormData();
    formData.append('name', document.getElementById('name').value);
    formData.append('age', document.getElementById('age').value);
    formData.append('photo', document.getElementById('photo').files[0]);

    // Envia o formulário para a API
    fetch('https://localhost:7249/api/v1/employee', {
        method: 'POST',
        body: formData
    })
    .then(response => {
        if (response.ok) {
            return response.text();
        }
        throw new Error('Erro ao cadastrar funcionário.');
    })
    .then(result => {
        alert('Funcionário cadastrado com sucesso!');
        document.getElementById('employee-form').reset(); // Limpa o formulário
    })
    .catch(error => {
        console.error('Error:', error);
        alert('Falha ao cadastrar funcionário. Verifique o console para detalhes.');
    });
});

document.getElementById('view-employees').addEventListener('click', function() {
    window.location.href = 'view-employees.html'; // Redireciona para a página de consulta
});

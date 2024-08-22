document.addEventListener('DOMContentLoaded', function() {
    fetch('https://localhost:7249/api/v1/employee?pageNumber=0&pageQuantity=10')
    .then(response => response.json())
    .then(data => {
        const employeeList = document.getElementById('employee-list');
        employeeList.innerHTML = ''; // Limpa qualquer conteúdo existente

        if (data.length === 0) {
            employeeList.innerHTML = '<p>Nenhum funcionário cadastrado.</p>';
            return;
        }

        data.forEach(employee => {
            const employeeDiv = document.createElement('div');
            employeeDiv.classList.add('employee');

            const name = document.createElement('h2');
            name.textContent = employee.name;

            const age = document.createElement('p');
            age.textContent = `Idade: ${employee.age}`;

            const photo = document.createElement('img');
            photo.src = `https://localhost:7249/api/v1/employee/${employee.id}/download`;
            photo.alt = 'Foto do Funcionário';
            photo.classList.add('employee-photo');

            employeeDiv.appendChild(name);
            employeeDiv.appendChild(age);
            employeeDiv.appendChild(photo);

            employeeList.appendChild(employeeDiv);
        });
    })
    .catch(error => {
        console.error('Error fetching employees:', error);
        document.getElementById('employee-list').innerHTML = '<p>Erro ao carregar funcionários.</p>';
    });

    document.getElementById('back').addEventListener('click', function() {
        window.location.href = 'index.html'; // Redireciona para a página de cadastro
    });
});

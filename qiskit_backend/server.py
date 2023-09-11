from flask import Flask, request, jsonify
from flask_cors import CORS
import qiskit
import matplotlib
matplotlib.use('Agg')
import numpy as np
import os # for environment variables
from dotenv import load_dotenv

# get the environment variables from the parent directory's .env file
load_dotenv(dotenv_path='../.env')

unity_port = os.getenv('COURSE_PORT', '8081')
flask_port = os.getenv('FLASK_PORT', '3000')
app = Flask(__name__)

CORS(app, resources={r"/*": {"origins": f"http://localhost:{unity_port}"}})

# we have to keep track of the state of the qubit 
current_state = qiskit.QuantumCircuit(1)  # global variable

@app.route('/reset', methods=['POST'])
def reset_qubit():
    # Reset qubit to the |0> state to start
    global current_state  # use global state
    current_state = qiskit.QuantumCircuit(1)  # Reset to a new circuit
    return jsonify({"message": "Qubit reset to |0> state"})


@app.route('/apply_gate', methods=['POST'])
def apply_gate():
    global current_state  # use global state

    try:
        data = request.json
        gate_name = data['gate_name']
        
        # Use Qiskit to apply the gate
        qc = current_state  # use the existing state
        
        if gate_name == 'hadamard':
            qc.h(0)
        elif gate_name == 'x':
            qc.x(0)
        elif gate_name == 'y':
            qc.y(0)
        elif gate_name == 'z':
            qc.z(0)
        elif gate_name == 's':
            qc.s(0)
        elif gate_name == 't':
            qc.t(0)
        else:
            return jsonify({"error": "Invalid gate name"})
        
        backend = qiskit.Aer.get_backend('statevector_simulator')
        result = qiskit.execute(qc, backend).result()
        state = result.get_statevector(qc)

        alpha = state[0]
        beta = state[1]

        # Calculate the Bloch vector components directly
        x = 2 * (alpha.real * beta.real + alpha.imag * beta.imag)
        y = 2 * (alpha.real * beta.imag - alpha.imag * beta.real)
        z = abs(alpha)**2 - abs(beta)**2

        bloch_vector = [x, y, z]

        # For debugging: plot the Bloch vector (this isn't needed for the server response)
        # plot_bloch_vector(bloch_vector)

        return jsonify({"bloch_vector": bloch_vector})
    except Exception as e:
        return jsonify({"error": str(e)})

if __name__ == '__main__':
    app.run(debug=os.getenv('DEBUG', 'false') == 'true', port=flask_port)

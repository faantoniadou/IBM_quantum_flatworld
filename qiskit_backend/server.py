from flask import Flask, request, jsonify
from flask_cors import CORS
import qiskit
from qiskit.visualization import plot_bloch_multivector

app = Flask(__name__)
CORS(app)

@app.route('/apply_gate', methods=['POST'])

def apply_gate():
    data = request.json
    gate_name = data['gate_name']  # Adjusted to match Unity's POST data
    
    # Use Qiskit here to apply the gate on Qubo's state, then calculate its position on the Bloch sphere.
    qc = qiskit.QuantumCircuit(1)
    
    if gate_name == 'hadamard':
        qc.h(0)

    backend = qiskit.Aer.get_backend('statevector_simulator')
    result = qiskit.execute(qc, backend).result()
    state = result.get_statevector(qc)
    
    # Get the Bloch sphere coordinates
    bloch = qiskit.visualization.plot_bloch_multivector(state, title="New State of Qubo", return_fig_data=True)
    
    # Return the bloch vector
    return jsonify({"bloch_vector": list(bloch)})

if __name__ == '__main__':
    app.run(debug=True)

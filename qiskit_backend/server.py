from flask import Flask, request, jsonify
from flask_cors import CORS
import qiskit
from qiskit.visualization import plot_bloch_vector
import matplotlib
matplotlib.use('Agg')


app = Flask(__name__)
CORS(app)

@app.route('/apply_gate', methods=['POST'])
def apply_gate():
    try:
      data = request.json
      gate_name = data['gate_name']
      
      # Use Qiskit to apply the gate
      qc = qiskit.QuantumCircuit(1)
      if gate_name == 'hadamard':
          qc.h(0)

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
    app.run(debug=True)

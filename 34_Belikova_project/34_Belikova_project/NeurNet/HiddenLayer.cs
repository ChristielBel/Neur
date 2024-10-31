namespace _34_Belikova_project.NeurNet
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn, NeuronType nt, string type) : base(non, nopn, nt, type)
        {

        }

        public override void Recognize(NetWork net, Layer nextLayer)
        {
            double[] hidden_out = new double[Neurons.Length];

            for(int i = 0; i < Neurons.Length; i++)
                hidden_out[i] = Neurons[i].Output;

            nextLayer.Data = hidden_out;
        }

        public override double[] BackwardPass(double[] stuff)
        {
            double[] gr_sum = new double[numofprevneurouns];

            // здесь пропишем обучение нейронной сети

            return gr_sum;
        }


    }
}

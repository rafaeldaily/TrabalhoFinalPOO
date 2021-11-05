using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace wfaTrabalhoFinal
{
    class Fila
    {
        //atributos
        private NohFila start;
        private NohFila end;
        private int count = 0;

        //construtor default
        public Fila()
        {
            start = null;
            end = null;
        }

        //função para verificar se esta vazia
        public bool isEmpty()
        {
            if (start == null)
                return true;
            else
                return false;

        }

        // 0) Verificar se a fila não está vazia
        // 1) Criar o nó
        // 2) Encadear com o novo nó
        // 3) Fazer FIM (start) apontar para novo nó




        public void insere(int valor)
        {
            NohFila novonoh = new NohFila(valor); // cria um novo NohFila

            if (isEmpty()) // a Fila está vazia -- primeiro nó da Fila
            {
                start = novonoh; // o start aponta para novonoh
                end = novonoh;   // o end aponta para novonoh
            }
            else
            {
                end.setAnterior(novonoh);
                end = novonoh;
            }

        } // fim do método insere( )

        public String imprime()
        {
            string temporary = string.Empty;
            ArrayList res = new ArrayList();
            if (isEmpty())
            {
                return "";
            }
            else
            {
                bool troca = true;
                NohFila aux = start;
                while (aux != null&&troca==true)
                {
                        res.Add(Convert.ToString(aux.getData()));
                        aux = aux.getAnterior();
                }

                res.Reverse();

                foreach (String var in res)
                {
                    temporary = temporary + var.ToString()+" ";
                }
                return temporary;
            }
        }

        public int retirar()
        {
            if (isEmpty())
            {
                Console.WriteLine("Fila Vazia");
                return 0;
            }

            else
            {
                int aux = start.getData();
                start = start.getAnterior();
                return (aux);

            }
        }

        public NohFila GetStart()
        {
            return start;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Core.Entities
{
	public abstract class Entidade
	{
		public Guid Id { get;private set; }

		public DateTime DataCriacao { get;private set; }

		public DateTime? DataAtualizacao { get; private set; }

		protected Entidade()
		{
			Id = Guid.NewGuid();
			DataCriacao = DateTime.Now;
		
		}

		public Entidade(Guid id) 
		{
			Id = id;
			DataAtualizacao = DateTime.Now;

		}

		public void AtualizarDataModificacao()
		{
			DataAtualizacao = DateTime.Now;
		}
	}
}

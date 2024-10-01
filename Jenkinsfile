pipeline {
	stages {
		stage('Clean workspace') {
			steps {
				cleanWs()
			}
		}

		stage('Build') {
			steps {
				dotnet publish --configuration Release
			}
		}
	}
}
